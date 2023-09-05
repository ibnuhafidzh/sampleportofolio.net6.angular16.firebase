import { HttpClient } from "@angular/common/http";
import { Component, NgZone, OnInit } from "@angular/core";
import { FacebookAuthProvider, GoogleAuthProvider, OAuthProvider } from "@angular/fire/auth";
import { AngularFireAuth, AngularFireAuthModule } from '@angular/fire/compat/auth';
import { AngularFirestore, AngularFirestoreModule } from '@angular/fire/compat/firestore';
import { Router } from "@angular/router";
import { environment } from "../../environments/environment";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    public afs: AngularFirestore, // Inject Firestore service
    public afAuth: AngularFireAuth, // Inject Firebase auth service
    public router: Router,
    public ngZone: NgZone,
    private http: HttpClient
  ) { }
  linkedInCredentials = {
    clientId: 'fillwithyour',
    redirectUrl: environment.redirecturl,
    scope: 'r_liteprofile%20r_emailaddress%20w_member_social', // To read basic user profile data and email
  };

  ngOnInit() { }

  loginFB = () => {
    return this.AuthLogin(new FacebookAuthProvider()).then((res: any) => { });
  };
  login() {
    window.location.href = `https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=${this.linkedInCredentials.clientId}&redirect_uri=${this.linkedInCredentials.redirectUrl}&state=foobar&scope=r_liteprofile%20r_emailaddress`;
  }
  loginGoggle = () => {
    return this.AuthLogin(new GoogleAuthProvider()).then((res: any) => {
      this.router.navigate(['/dashboard'], {
        queryParams: { code: 'google' },
      });
    });
  };
  AuthLogin(provider: any) {
    return this.afAuth
      .signInWithPopup(provider)
      .then((result) => {
        // console.log(result.user?._delegate.accessToken)


        this.router.navigate(['dashboard']);
      })
      .catch((error) => {
        this.router.navigate(['home']);
      });
  }
  loginMicrosoft = () => {
    return this.AuthLogin(new OAuthProvider('microsoft.com')).then(
      (res: any) => { }
    );
  };

}
