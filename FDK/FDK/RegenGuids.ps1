#
# The script was written by Victor Marmysh.
#
# I sometimes need to regenerate all guids in FDK installation project.
# The main cause of this is the following we allow to install to different version of
# FDK at the same time. In this case if components from different installation
# have the same guid then user can't uninstall EMC-Analyzer correct
#



# creates a new regular expression for GUIDs detecting
$patten = New-Object System.Text.RegularExpressions.Regex -ArgumentList "[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}", IgnoreCase

Function RegenerateGuids($path)
{
	"Processing: " + $path
	$lines = Get-Content $path;
	Remove-Item $path
	foreach($element in $lines)
	{
		$new = [System.Guid]::NewGuid().ToString();
		$element = $patten.Replace($element, '*');
		$element | Out-File -FilePath $path -Encoding ASCII -Append	
	}
}

clear;
$files = Get-ChildItem -Filter *.wx? -Recurse -Force;
foreach($element in $files)
{
	$status = $element.IsReadOnly;
	if($status)
	{
		"File (" + $element.FullName + ") has been skipped, because it is readonly";
	}
	else
	{
		RegenerateGuids($element.FullName);	
	}
}





