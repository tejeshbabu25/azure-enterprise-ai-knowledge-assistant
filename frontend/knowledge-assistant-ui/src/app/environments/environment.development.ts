export const environment = {
  production: false,
   azureAd :{
    tenantId:'d0fec16a-a49b-4f55-9d0f-4b83555f500c',
    clientId:'63bb9862-457f-45b1-aaa3-c3e5e974ac48',
    authority:'https://login.microsoftonline.com/d0fec16a-a49b-4f55-9d0f-4b83555f500c',
    redirectUri:'http://localhost:4200',
    postLogoutRedirectUri:'http://localhost:4200',
  },
  api:{
    baseUrl:'https://localhost:7082/api',
    scope:'api://21a7ae2e-14a3-4955-b777-5c9a9a0a6738/access_as_user'
  }
};