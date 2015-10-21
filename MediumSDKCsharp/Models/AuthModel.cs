using System;
using System.Collections.Generic;
using System.Web;
using RestSharp.Deserializers;

namespace MediumSDKCsharp.Models {
	public enum ScopeTypes {
		basicProfile,
		publishPost,
		uploadImage
	}


	public class AuthInit {
		[DeserializeAs(Name = "client_id")]
		public string ClientId { get; set; }

		[DeserializeAs(Name = "scope")]
		public IEnumerable<ScopeTypes> Scopes { get; set; }

		[DeserializeAs(Name = "state")]
		public string State { get; set; }

		[DeserializeAs(Name = "redirect_uri")]
		public Uri RedirectUri { get; set; }

		[DeserializeAs(Name = "response_type")]
		public string ResponseType => "code";

		public override string ToString() {
			var parser = HttpUtility.ParseQueryString(string.Empty);

			parser["client_id"] = ClientId;
			parser["scope"] = string.Join(",", Scopes);
			parser["state"] = State;
			parser["response_type"] = ResponseType;
			parser["redirect_uri"] = RedirectUri.ToString();

			return parser.ToString();
		}
	}

	public class AuthTokenRequest {
		public string code { set; get; }
		public string client_id { set; get; }
		public string client_secret { set; get; }
		public string grant_type => "authorization_code";
		public string redirect_uri { set; get; }
	}

	public class AuthTokenResponse {
		public string token_type => "Bearer";
		public string access_token { set; get; }
		public string refresh_token { set; get; }
		public List<ScopeTypes> scope{ set; get; }
		public Int64 expires_at { set; get; }
	}

	public class RefreshTokenRequest {
		public string refresh_token { get; set; }
		public string client_id { get; set; }
		public string client_secret { get; set; }
		public string grant_type => "refresh_token";
	}
}
