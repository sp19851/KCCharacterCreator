fx_version 'bodacious'
game 'gta5'



files {
	'Build/Client/*.dll',
	'ui/index.html',
	'ui/scripts/*.js',
	'ui/assets/*.png',
	'ui/assets/img/*.png',
	'ui/assets/img/backgrounds/*.png',
	'ui/assets/icons/*.png',
	'ui/scss/*.css',
	'ui/fonts/*.ttf',
 -- CONFIG --
    '*.json',
    'config/*.json',
	    
	
}

ui_page 'ui/index.html'
client_script {
'Build/Client/*.net.dll',
--'Build/Shared/bin/Release/*.net.dll',

}
--server_script 'Build/Server/bin/Release/**/publish/*.net.dll'

author 'Cruso'
version '0.0.0'
description 'CharacterCreater for KCore on FiveM Game Server'