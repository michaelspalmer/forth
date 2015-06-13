\ Constants
70 constant WIDTH
30 constant HEIGHT
WIDTH HEIGHT * constant LENGTH
50 constant PE

\ Arrays to hold the organisms
variable PLANTS LENGTH CELLS ALLOT
variable ANIMALS LENGTH CELLS ALLOT

\ reset button
: reset_all ( -- )
    PLANTS LENGTH cells erase
    ANIMALS LENGTH cells erase
    clearstack
    page
    cr ." all variables reset" cr ;

\ Location words, random number generation
: xy_to_loc ( x y -- loc )
    WIDTH * + ;
    
: rng8 ( -- rn )
    8 random 1+ ;
    
: 8Rnd ( -- 8 rns )
    8 0 
    do 
        rng8 
    loop 
    ;
    
: rngW ( -- rn )
    WIDTH random ;
    
: rngH ( -- rn )
    HEIGHT random ;
    
: center ( -- n )
    WIDTH 2 / HEIGHT 2 / xy_to_loc ;
    
: rndloc ( -- n )
    rngW rngH xy_to_loc ;

\ Get thing at index
: @idx ( [thing] index --> thing )
    cells + @ ;
    
: hasplant? ( loc -- n )
    PLANTS swap @idx ;

: hasanimal? ( loc -- n )
    ANIMALS swap @idx ;
    
\ show the array
: aloop ( -- )
    LENGTH 0 
    do 
        ANIMALS i 
        @idx 
        dup 
        if 
            drop 
            ." A" 
        else 
            drop 
            ." _" 
        then 
    loop ;
: ploop ( -- )
    LENGTH 0 
    do 
        PLANTS i 
        @idx 
        dup 
        if 
            drop 
            ." P" 
        else 
            drop 
            ." _" 
        then 
    loop ;
: arrayloop aloop ploop ;

\ Draw the world
: show_world ( -- )
    page
    2 2 at-xy
    HEIGHT 0
    do
        cr ." |"
        WIDTH 0
        do
            i j
            2dup xy_to_loc
            ANIMALS swap 
            @idx 0
            or
            if
                ." M"
            else
                xy_to_loc 
                PLANTS swap 
                @idx 0 
                or
                if
                    ." 4"
                else
                    space
                then
            then
        loop
        ." |"
    loop
    clearstack
    ;
    
