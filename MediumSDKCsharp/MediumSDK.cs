using System;
using MediumSDKCsharp.Models;
using RestSharp;

namespace MediumSDKCsharp {
	public class MediumSdk {
		private const string Basepath = "https://api.medium.com";
		private const string Version = "v1";

		private readonly string _applicationId;
		private readonly string _applicationSecret;
		private string _accessToken;

		public MediumSdk(string applicationId, string applicationSecret, string accessToken = null) {
			_applicationId = applicationId;
			_applicationSecret = applicationSecret;
			_accessToken = accessToken;
		}

		public string GetAuthUrl(string state, string redirectUrl, ScopeTypes[] scopes) {
			var qs = new AuthInit {
				ClientId = _applicationId,
				Scopes = scopes,
				State = state,
				RedirectUri = new Uri(redirectUrl)
			};
			var uri = new UriBuilder("https://medium.com/m/oauth/authorize?") {Query = qs.ToString()};
			return uri.ToString();
		}

		public void ExhangeAuthCodeForToken(AuthTokenRequest authTokenRequest) {
			string path = $"/{Version}/tokens";
			var request = new RestRequest(path) {Method = Method.POST};
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("client_id", _applicationId, ParameterType.GetOrPost);
			request.AddParameter("client_secret", _applicationSecret, ParameterType.GetOrPost);
			request.AddParameter("code", authTokenRequest.code, ParameterType.GetOrPost);
			request.AddParameter("redirect_uri", authTokenRequest.redirect_uri, ParameterType.GetOrPost);
			request.AddParameter("grant_type", authTokenRequest.grant_type, ParameterType.GetOrPost);
			var response = Request<AuthTokenResponse>(request);
			_accessToken = response.access_token;
		}

		public CreatePostResponse CreatePost(CreatePostRequest createPostRequest, string userId) {
			var path = $"/{Version}/users/{userId}/posts";

			var request = new RestRequest(path) {
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};
			var mappedRequest = new MapCreatePostRequest(createPostRequest);
			request.AddJsonBody(mappedRequest);
			return Request<CreatePostResponse>(request);
		}

		public UserResponse GetUser() {
			string path = $"/{Version}/me";

			var request = new RestRequest(path);
			request.AddHeader("Content-Type", "application/json");
			request.Method = Method.GET;
			request.RootElement = "data";
			return Request<UserResponse>(request);
		}

		private T Request<T>(IRestRequest request) where T : new() {
			var client = new RestClient(Basepath);
			request.AddHeader("Accept", "application/json");
			request.AddHeader("Accept-Charset", "utf-8");
			request.AddHeader("Authorization", $"Bearer {_accessToken}");
			var response = client.Execute<T>(request);
			if (response.ErrorException != null) {
				const string message = "Error retrieving response.  Check inner details for more info.";
				var exception = new ApplicationException(message, response.ErrorException);
				throw exception;
			}
			return response.Data;
		}
	}
}