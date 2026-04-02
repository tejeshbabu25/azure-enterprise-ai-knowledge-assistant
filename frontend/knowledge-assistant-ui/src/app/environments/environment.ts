export const environment = {
  production: true,
   azureAd :{
    tenantId:'d0fec16a-a49b-4f55-9d0f-4b83555f500c',
    clientId:'63bb9862-457f-45b1-aaa3-c3e5e974ac48',
    authority:'https://login.microsoftonline.com/d0fec16a-a49b-4f55-9d0f-4b83555f500c',
    redirectUri:'https://victorious-pond-0475f930f.2.azurestaticapps.net/',
    postLogoutRedirectUri:'https://victorious-pond-0475f930f.2.azurestaticapps.net/',
  },
  api:{
    baseUrl:'https://api-knowledge-assistant-aqebcea3atgmcbfm.centralus-01.azurewebsites.net/api',
    scope:'api://21a7ae2e-14a3-4955-b777-5c9a9a0a6738/access_as_user'
  }
};