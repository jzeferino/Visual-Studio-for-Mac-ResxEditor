using Mono.Addins;

[assembly:Addin (
	"ResxEditor", 
	Namespace = "ResxEditor",
	Version = "0.2.0"
)]

[assembly:AddinName ("ResxEditor")]
[assembly:AddinCategory ("IDE extensions")]
[assembly:AddinAuthor ("Caleb Morris")]
[assembly:AddinUrl("https://github.com/CalebMorris/Xamarin-ResxEditor")]
[assembly:ImportAddinAssembly("ResxEditor.Core.dll")]
[assembly:AddinDescription ("Resx editor for simplified resource handling.\n\nCurrently only works with string resources.\n\nSupport: https://github.com/CalebMorris/Xamarin-ResxEditor")]
