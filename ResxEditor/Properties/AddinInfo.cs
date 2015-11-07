using Mono.Addins;

[assembly:Addin (
	"ResxEditor", 
	Namespace = "ResxEditor",
	Version = "0.0.1"
)]

[assembly:AddinName ("ResxEditor")]
[assembly:AddinCategory ("IDE extensions")]
[assembly:AddinAuthor ("Caleb Morris")]
[assembly:AddinDescription ("Resx editor for simplified resource handling")]

//AssemblyName assemblyeInfo = AssemblyName.GetAssemblyName("ResxEditor.Core.dll");

[assembly:ImportAddinAssembly("ResxEditor.Core.dll")]
