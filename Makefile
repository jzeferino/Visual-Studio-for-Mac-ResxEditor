MDTOOL = "/Applications/Visual Studio.app/Contents/Resources/lib/monodevelop/bin/vstool.exe"

all: build-release

clean:clean-build clean-addins

clean-build:
	rm -Rfv **/{obj,bin}

clean-addins:
	rm -fv ResxEditor.*.mpack
	rm -fv **/ResxEditor.*.mpack

build-release:
	mono $(MDTOOL) build ResxEditor.sln -c:Release

build-debug:
	mono $(MDTOOL) build ResxEditor.sln -c:Debug

pack-addin: clean build-release
	mono $(MDTOOL) setup pack src/ResxEditor/bin/Release/ResxEditor.dll -d:.
