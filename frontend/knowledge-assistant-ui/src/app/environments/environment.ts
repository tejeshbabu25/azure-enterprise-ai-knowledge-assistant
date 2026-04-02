export const environment = {
  production: true,
   azureAd :{
    tenantId:'<your-tenant-id>',
    clientId:'<your-client-id>',
    authority:'https://login.microsoftonline.com/d0fec16a-a49b-4f55-9d0f-4b83555f500c',
    redirectUri:'<your-azure-static-web-app-url>',
    postLogoutRedirectUri:'<your-azure-static-web-app-url>',
  },
  api:{
    baseUrl:'<your-api-url/api>',
    scope:'api://21a7ae2e-14a3-4955-b777-5c9a9a0a6738/access_as_user'
  }
};