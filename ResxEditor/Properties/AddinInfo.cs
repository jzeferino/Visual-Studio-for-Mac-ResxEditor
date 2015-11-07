using Mono.Addins;

[assembly:Addin (
	"ResxEditor", 
	Namespace = "ResxEditor",
	Version = "0.0.2"
)]

[assembly:AddinName ("ResxEditor")]
[assembly:AddinCategory ("IDE extensions")]
[assembly:AddinAuthor ("Caleb Morris")]
[assembly:AddinDescription ("Resx editor for simplified resource handling.\n Currently only works with string resources.")]
[assembly:AddinUrl("https://github.com/CalebMorris/Xamarin-ResxEditor")]
[assembly:ImportAddinAssembly("ResxEditor.Core.dll")]
