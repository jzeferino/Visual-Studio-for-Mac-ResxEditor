using Mono.Addins;

[assembly:Addin (
	"ResxEditor", 
	Namespace = "ResxEditor",
	Version = "0.2.0"
)]

[assembly:AddinName ("ResxEditor")]
[assembly:AddinCategory ("IDE extensions")]
[assembly:AddinAuthor ("Caleb Morris; jzeferino")]
[assembly:AddinUrl("https://github.com/jzeferino/Visual-Studio-for-Mac-ResxEditor")]
[assembly:ImportAddinAssembly("ResxEditor.Core.dll")]
[assembly:AddinDescription ("Resx editor for simplified resource handling.")]
