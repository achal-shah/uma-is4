# uma-is4
Experiments with UMA (User Managed Access) and IdentityServer4.


## About this project
The classic OAuth flow allows a user to allow one service to access the user's resources on another.  The canonical example is that of a user who has a photo album on the
cloud and wants to allow a printing service to access these pictures to get them printed.  

What about person to person sharing?  The example is a user, Alice who wishes to share her tax returns with her accountant and her financial planner.  The accountant needs "write" 
access since he is going to ensure that nothing has been overlooked.  The planner just needs to "read" them so that he can advise Alice on appropriate investing.  Alice stores her 
tax returns on an online service.  How can she provide the right level of access *and* ensure that no one else apart from the individuals she has authorized has the access?

## User Managed Access (UMA)
The solution is provided by this standard.  The actors in this model are:
- The resource *owner* - the person who owns a resource.
- The resource *server* - the service which hosts the resource.
- The requesting party - the person to whom the resource owner wishes to authorize access.
- The authorization server - a service which is used to manage authorization and access to resources.

The resource owner, through the resource server registers the resource with the authorization server. The owner then inteacts with the authorization server to set access
polcies.  The requesting party, through its own client service, interacts with the authorization server and resource server to validate and get access to the resources.
