import { HttpClient } from '@angular/common/http';
import { Component, Inject, Pipe, PipeTransform, } from '@angular/core';
import { DomSanitizer } from "@angular/platform-browser";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DetailexperienceComponent } from '../detailexperience/detailexperience.component';
import { Experience } from '../model/Experience';

@Pipe({ name: "safeHtml" })
export class SafeHtmlPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }

  transform(value: string) {
    return this.sanitizer.bypassSecurityTrustHtml(value);
  }
}
@Component({
  selector: 'app-experience',
  templateUrl: './experience.component.html',
  styleUrls: ['./experience.component.css']
})
export class ExperienceComponent {
  public forecasts: Experience[] = [];
  public forecastsori: Experience[] = [];
  #basesurl = '';
  constructor(private modalService: NgbModal, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.#basesurl = baseUrl + 'weatherforecast/getexperience';
    http.get<Experience[]>(this.#basesurl).subscribe(result => {
      this.forecasts = result;
      this.forecastsori = result;
    }, error =>
      console.error(""));
  }
  open(lesson: Experience) {
    const modalRef = this.modalService.open(DetailexperienceComponent);
    modalRef.componentInstance.lesson = lesson;
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    let filterValueLower = filterValue.toLowerCase();
    if (filterValue === '') {
      this.forecasts = this.forecastsori;
    } else {
      this.forecasts = this.forecasts.filter((employee) =>
        employee.name.toLowerCase().includes(filterValueLower) ||
        employee.type.toLowerCase().includes(filterValueLower) ||
        employee.detail.toLowerCase().includes(filterValueLower));
    }
  }
}
