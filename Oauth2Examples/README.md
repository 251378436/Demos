## Introduction
These examples demonstrate how OAuth2 is used in asp.net api.

OAuth 2.0, which stands for "Open Authorization", is a standard designed to allow a website or application to access resources hosted by other web apps on behalf of a user. 

This demo uses Auth0 as the OAuth server provider. You can use any providers or create by yourself.

### First API - Asp.Net web API:
There are two enpoints in this example:

Enpoint 1: If inbound request is authorized, then return result directly (Inbound Auth only)

Enpoint 2: If inbound request is authorized, then send a http request with bearer token to second API (Inbound and Outbound Auth)

### Second API - Asp.Net web API:
There are one enpoint in this example:

Enpoint 1: If inbound request is authorized, then return result directly (Inbound Auth only)

## References
* [**Quick start Auth0 with asp.net web api.**](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/interactive)
* [**An example how to implement robe based access with permissions(scopes) in asp.net.**](https://medium.com/projectwt/web-api-in-net-6-0-with-auth0-with-roles-and-permissions-50103832ea21)
* [**Explanation of OAuth2.**](https://medium.com/web-security/understanding-oauth2-a50f29f0fbf7)
* [**Difference between OAuth2 and OIDC.**](https://www.linkedin.com/advice/0/how-do-you-choose-between-oauth2-openid#:~:text=The%20main%20difference%20between%20OAuth2,the%20identity%20of%20a%20user.)

