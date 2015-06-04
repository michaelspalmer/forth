\ Parameters for Create
: Four, ( n1 n2 n3 n4 -- )
    , , , , ;
: Eight, ( 8 rns -- compiled )
    , , , , , , , , ;

\ Structures - genes, animals
: ^gen ( / str -- str->[8 * n] )
    8Rnd CREATE Eight, ;
: ^ani ( n1 n2 n3 n4 / str -- str->[n4 n3 n2 n1] )
    CREATE Four, ;

\ get and set fields
: @loc ( animal -- n )
    @ ;
: >loc ( animal n --  )
    swap ! ;
: @enr ( animal -- n )
    1 cells + @ ;
: >enr ( animal n -- )
    swap 1 cells + +! ;
: @dir ( animal -- n )
    2 cells + @ ;
: >dir ( animal n -- )
    swap 2 cells + +! ;
    
\ get the address of the gene array
: @gen ( animal -- [n] )
    3 cells + @ ;
\ get the gene at the given index
: idx@gen ( animal index -- n ) 
    swap @gen swap cells + @ ;
\ loop through and print genes - for debug
: gen_lp ( animal -- )
    cr 
    8 0 
    do 
        dup i 
        idx@gen . 
        cr 
    loop ;

\ push to array
: rev3 ( n1 n2 n3 -- n3 n2 n1 )
    swap rot ;
: >>ani ( animal [animal] -- )
    over @loc cells + ! ;
: >>plt ( loc [plants] -- )
    1 rev3 cells + ! ;
: >>rndplt ( -- )
    1 ps rndloc cells + ! ;

    
: hasplant? ( loc -- n )
    ps swap @idx ;
    
: a/eat ( a -- )
    dup >r
    @loc hasplant?
    r>
    swap 0 
    or
    if
        PE >enr
    else
        drop
    then ;


\ : >>rndani ( -- )
\    ^gen gs gs 0 500 rndloc ^ani an an as >>ani ;


\ increment / decrement counts - not used
: -cntr ( counter -- )
    1 swap +! ;
: +cntr ( counter -- )
    -1 swap +! ;