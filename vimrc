" this will unset unexpected things, and reset options when resourcing .vimrc

set nocompatible



" Try to figure out what kind of file the file is

filetype indent plugin on



" syntax highlighting

syntax on



" better command line completion

set wildmenu



" show partial commands in the last line of the screen

set showcmd



" use case insensitive search,

set ignorecase



" except when using capital letters

set smartcase



" allow backspacing over autoindent, line breaks, and start of insert action

set backspace=indent,eol,start



" stop certain movements from always going to the first character of a line

set nostartofline 



" always display the status line

set laststatus=2



" confirm commands that might ruin things (like unsaved changes)

set confirm



" uses the visual bell instead of making a noise

set visualbell



" enable mouse for all modes

set mouse=a



" height of the command line at the bottom

set cmdheight=3



" display line numbers

set number



" quickly timeout keycodes, but never timeout on mappings

set notimeout ttimeout ttimeoutlen=200



" turn on highlighting

set hls



colorscheme slate



" dark background

set background=dark


