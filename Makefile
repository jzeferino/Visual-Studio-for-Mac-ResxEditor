MDTOOL = "/Applications/Xamarin Studio.app/Contents/MacOS/mdtool"

all: build-release

clean:clean-build clean-addins

clean-build:
	rm -Rfv **/{obj,bin}

clean-addins:
	rm -fv **/ResxEditor.*.mpack

build-release:
	$(MDTOOL) build ResxEditor.sln -c:Release

build-debug:
	$(MDTOOL) build ResxEditor.sln -c:Debug

pack-addin: clean build-release
	$(MDTOOL) setup pack ResxEditor/bin/Release/ResxEditor.dll -d:.

analyze: build-debug
	mono packages/Mono.Gendarme.*/tools/gendarme.exe ResxEditor/bin/Debug/ResxEditor.dll ResxEditor.Core/bin/Debug/ResxEditor.Core.dll
