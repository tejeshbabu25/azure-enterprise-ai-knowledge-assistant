import { Component,OnInit,inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: `<router-outlet></router-outlet>`,
})
export class AppComponent implements OnInit{
  private readonly msalService = inject(MsalService);
  ngOnInit(): void {
    this.msalService.instance.handleRedirectPromise({
      navigateToLoginRequestUrl: true
    })
    .then((result: AuthenticationResult | null) => {
      if (result?.account) {
        this.msalService.instance.setActiveAccount(result.account);
      } else {
        const accounts = this.msalService.instance.getAllAccounts(); 
        if(accounts.length > 0) {
          this.msalService.instance.setActiveAccount(accounts[0]);
        }
      }
  })
  .catch((error) => {
    console.error('MSAL redirecthandling redirect:', error);
  });
}
}