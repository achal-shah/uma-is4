# uma-is4
Learning UMA (User Managed Access) using IdentityServer4.


## About User Managed Access (UMA)
The classic OAuth flow allows a user to allow one service to access the user's resources on another.  The canonical example is that of a user who has a photo album on the
cloud and wants to allow a printing service to access these pictures to get them printed.  

UMA addresses person to person sharing.  The example is a user, Alice who wishes to share her tax returns with her accountant and her financial planner.  The accountant 
needs "write" access since he is going to help with preparation to ensure that nothing has been overlooked.  The planner needs to "read" them to obtain information feeding into
the planning process.  Alice uses an online tax service to prepare and store her returns.  How can she provide the right level of access *and* ensure that no one else 
apart from the individuals she has authorized has that access?

The actors in this model are:
- The resource *owner* - the person who owns a resource.
- The resource *server* - the service which hosts the resource.
- The requesting party - the person to whom the resource owner wishes to authorize access.
- The authorization server - a service which is used to manage authorization and access to resources.

The resource owner, through the resource server registers the resource with the authorization server. The owner then inteacts with the authorization server to set access
polcies.  The requesting party, through its own client service, interacts with the authorization server and resource server to validate and get access to the resources.

The UMA specs are maintained by the [Kantara Initiative](https://kantarainitiative.org/groups/user-managed-access-work-group/).
The two main specs are:
1. [Federated Authorization for UMA 2.0](https://docs.kantarainitiative.org/uma/wg/rec-oauth-uma-federated-authz-2.0.html) - addressing resource protection.
2. [UUMA 2.0 Grant for OAuth 2.0 Authorization](https://docs.kantarainitiative.org/uma/wg/rec-oauth-uma-grant-2.0.html) - addressing the requesting party operations.

## About this project
This project uses IdentityServer4 as the authorization server and a simple MVC web application as the resource server. The IS4 project was bootstrapped using their quickstarts [here](https://identityserver4.readthedocs.io/en/latest/quickstarts/0_overview.html).

### Project Goals
There are 3 goals:
1. The initial goal is to simply put together enough code to learn UMA.  The solution would run locally on a developer machine.  <-- Where we are, a work in progress.
2. The medium term goal is to set up a non-production system in the cloud to learn about real world interactions and integration.
3. The long term goal is a production system in the cloud which can be used by hobbyists.
