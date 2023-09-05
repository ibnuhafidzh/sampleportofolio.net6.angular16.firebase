import { HttpClient } from '@angular/common/http';
import { Component, Inject, Pipe, PipeTransform, } from '@angular/core';
import { DomSanitizer } from "@angular/platform-browser";
import { About } from '../model/About';

@Pipe({ name: "safeHtml" })
export class SafeHtmlPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }

  transform(value: string) {
    return this.sanitizer.bypassSecurityTrustHtml(value);
  }
}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public forecasts!: About;
  #basesurl = '';
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.#basesurl = baseUrl + 'weatherforecast/getpersonal';
    http.get<About>(this.#basesurl).subscribe(result => {
      this.forecasts = result;
    }, error =>
      console.error(""));
  }
}
