using MediumSDKCsharp;
using MediumSDKCsharp.Models;

namespace MediumTestClient
{
    public class Program {

	    private const string Clientid = "YOUR_CLIENT_ID";
	    private const string Clientsecret = "YOUR_CLIENT_SECRET";
	    private const string Token = "TOKEN_AFTER_CODE_EXCHANGE";
	    private const string RedirectUri = "http://your_callback_url/";
        public static int Main() {

	        return 0;
        }

	   
	    private void AuthorizeExample() {
		    var mc = new MediumSdk(Clientid, Clientsecret);
		    var authUrl = mc.GetAuthUrl("secretstate", RedirectUri,new[] {ScopeTypes.basicProfile, ScopeTypes.publishPost});
			//Visit the above authUrl in a browser to get your code.
			
	    }

	    private static MediumSdk ExhangeCodeForTokenExample() {
			var mc = new MediumSdk(Clientid, Clientsecret);
			var authCode = "CODE_FROM_AUTH_URL_VISIT";
		    var authToken = new AuthTokenRequest {
			    code = authCode,
			    redirect_uri = RedirectUri
		    };
		    var tokenResponse = mc.ExhangeAuthCodeForToken(authToken);
		    return mc;
	    }

		private void CreatePostExample() {
			var mc = new MediumSdk(Clientid, Clientsecret, Token);
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
		}

	}
}
