
function Prebuild ()
{
	Remove-ExtraDir
}
 
function Postbuild ()
{
	Create-ExtraDir
	Pack-NuGet
}

