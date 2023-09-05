import { Pipe } from '@angular/core';
import { PipeTransform } from '@angular/core';
import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Experience } from '../model/Experience';
@Pipe({ name: "safeHtml" })
export class SafeHtmlPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }

  transform(value: string) {
    return this.sanitizer.bypassSecurityTrustHtml(value);
  }
}
@Component({
  selector: 'app-detailexperience',
  templateUrl: './detailexperience.component.html',
  styleUrls: ['./detailexperience.component.css']
})
export class DetailexperienceComponent {
  @Input()
  lesson!: Experience;
  constructor(public activeModal: NgbActiveModal) { }


}


