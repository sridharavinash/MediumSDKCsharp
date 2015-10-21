using System;

namespace MediumSDKCsharp.Models {
	public enum PostContentFormat {
		html,
		markdown
	}

	public enum PublishStatuses {
		Public,
		draft,
		unlisted
	}

	public enum Licenses {
		all_rights_reserved,
		cc_40_by,
		cc_40_by_sa,
		cc_40_by_nd,
		cc_40_by_nc,
		cc_40_by_nc_nd,
		cc_40_by_nc_sa, 
		cc_40_zero,
		public_domain
	}

	public class CreatePostRequest {
		public string title { get; set; }
		public string content { get; set; }
		public PostContentFormat contentFormat { get; set; }
		public string[] tags { get; set; }
		public string canonicalUrl { get; set; }
		public PublishStatuses publishStatus { get; set; }
		public Licenses license { get; set; }

	}
	/// <summary>
	/// This is a helper class that converts the public post object to a JSON friendly request. i.e fixes the enum parsing
	/// so that they are correctly JSON formatted.
	/// </summary>
	internal class MapCreatePostRequest {
		private readonly CreatePostRequest _baseCr;

		internal MapCreatePostRequest(CreatePostRequest cr) {
			_baseCr = cr;
		}
		public string title => _baseCr.title;
		public string content => _baseCr.content;
		public string contentFormat => _baseCr.contentFormat.ToString();
		public string[] tags => _baseCr.tags;
		public string canonicalUrl => _baseCr.canonicalUrl;
		public string publishStatus => _baseCr.publishStatus.ToString().ToLower();
		public string license => _baseCr.license.ToString().Replace('_', '-');

	}

	public class CreatePostResponse {
		public string id { get; set; }
		public string title { get; set; }
		public string authorId { get; set; }
		public string[] tags { get; set; }
		public string url { get; set; }
		public string canonicalUrl{ get; set; }
		public PublishStatuses publishStatus { get; set; }
		public DateTime publishedAt { get; set; }
		public PostContentFormat contentFormat { get; set; }
		public Licenses license { get; set; }
		public string licenseUrl { get; set; }

	}
}