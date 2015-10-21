# MediumSDKCsharp
A C# wrapper for accessing the Medium API.

#Quick Start
 ```csharp
var mc = new MediumSdk(Clientid, Clientsecret);
var authUrl = mc.GetAuthUrl("secretstate", RedirectUri,new[] {ScopeTypes.basicProfile, ScopeTypes.publishPost});
//Visit the above authUrl in a browser to get your code.

var authCode = "CODE_FROM_AUTH_URL_VISIT";
var authToken = new AuthTokenRequest {
			    code = authCode,
			    redirect_uri = RedirectUri
		    };
//Get a long lived token for the auth code
mc.ExhangeAuthCodeForToken(authToken);

var user = mc.GetUser();
var postRequest = new CreatePostRequest
			{
				title = "Test from API",
				content = "<p>This is a test<p>",
				contentFormat = PostContentFormat.html,
				publishStatus = PublishStatuses.draft,
				tags = new[] { "api", "test" }
			};
var post = mc.CreatePost(postRequest, user.id);
