: dump-array ( addr cell-count -- )
    cells over + swap \ creates a start/end for this array n spaces from the start
        do cr i @ 5 u.r
    1 cells +loop ;

: dump-array-to-stack ( addr cells-count -- [cell values] )
    cells over + swap
        do cr i @
    1 cells +loop ;

create BOARD 9 allot

: square ( square# -- addr )
    ( square# ) BOARD + ; ( addr )
    
: clear BOARD 9 erase ;

clear

: bar ." | " ;

: dashes 
    cr 9 0 
    do 
        [char] - emit 
    loop 
    cr ;

: .box ( square# -- )
    square c@ 
    dup 0=
        if
            2 spaces 
        else 
            dup 1 = 
                if 
                    ." X " 
                else 
                    ." O " 
                then 
        then 
    drop ;
    
: display ( -- )
    cr 9 0 
    do 
        i 
        if 
            i 3 mod 0= 
                if 
                    dashes 
                else 
                    bar 
                then 
        then 
            i .box 
    loop 
    cr quit ;

: play ( player square# -- )
    1- 0 max 8 min square c! ;

: x! ( square# -- )  1 swap play display ;

: o! ( square# -- ) -1 swap play display ;