import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PDFDocumentProxy } from 'ng2-pdf-viewer';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  loginprovider = '';
  src = '';
  isPdfLoaded = false;
  private pdf: PDFDocumentProxy;

  onLoaded(pdf: PDFDocumentProxy) {
    this.pdf = pdf;
    this.isPdfLoaded = true;
  }
  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.src = 'changetoyourpdfurl';
    var querypar = this.route.snapshot.queryParams['code'];
    if (querypar != null && querypar != undefined) {
      if (querypar == 'google') {
        this.loginprovider = 'google';
      } else {
        this.router.navigate(['/home']);
      }
    } else {
      this.router.navigate(['/home']);
    }
  }
  print() {
    this.pdf.getData().then((u8) => {
      let blob = new Blob([u8.buffer], {
        type: 'application/pdf',
      });

      const blobUrl = window.URL.createObjectURL(blob);
      const iframe = document.createElement('iframe');
      iframe.style.display = 'none';
      iframe.src = blobUrl;
      document.body.appendChild(iframe);
      iframe.contentWindow.print();
    });
  }
}
