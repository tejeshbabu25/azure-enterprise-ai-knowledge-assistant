import { Injectable,inject } from "@angular/core";
import { MsalService } from "@azure/msal-angular";
import { RedirectRequest,AccountInfo } from "@azure/msal-browser";
import { environment } from "../environments/environment.development";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly msalService = inject(MsalService);

  login(): void {
    const loginRequest: RedirectRequest = {
      scopes: ['openid', 'profile', 'offline_access', environment.api.scope]
    };
    this.msalService.loginRedirect(loginRequest);
  }

  logout(): void {
    this.msalService.logoutRedirect({
      postLogoutRedirectUri: environment.azureAd.postLogoutRedirectUri
    });
  }

  getActiveAccount(): AccountInfo | null {
    return this.msalService.instance.getActiveAccount();
  }

  isLoggedIn(): boolean {
    return this.msalService.instance.getAllAccounts().length > 0;
  }

  getUserName(): string | undefined {
    const activeAccount = this.msalService.instance.getActiveAccount();
    if(activeAccount?.username) {
      return activeAccount.username;
    }
    const accounts = this.msalService.instance.getAllAccounts();
    if(accounts.length > 0) {
        if(!activeAccount){
            this.msalService.instance.setActiveAccount(accounts[0]);
        }
        return accounts[0].username??'';
    }

    return '';
  }
}
