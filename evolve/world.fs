\ Constants
70 constant W
30 constant H
W H * constant L
50 constant PE

\ Arrays to hold the organisms
variable ps L CELLS ALLOT
variable as L CELLS ALLOT

\ reset button
: reset_all ( -- )
    ps  L cells erase
    as L cells erase
    clearstack
    page
    cr ." all variables reset" cr ;

\ Location words, random number generation
: xy_to_loc ( x y -- loc )
    W * + ;
: rng8 ( -- rn )
    8 random 1+ ;
: 8Rnd ( -- 8 rns )
    8 0 do rng8 loop ;
: rngW ( -- rn )
    W random ;
: rngH ( -- rn )
    H random ;
: center ( -- n )
    W 2 / H 2 / xy_to_loc ;
: rndloc ( -- n )
    rngW rngH xy_to_loc ;

\ Get thing at index
: @idx ( [thing] index --> thing )
    cells + @ ;
    
\ show the array
: aloop ( -- )
    L 0 
    do 
        as i 
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
    L 0 
    do 
        ps i 
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
    H 0
    do
        cr ." |"
        W 0
        do
            i j
            2dup xy_to_loc
            as swap 
            @idx 0
            or
            if
                ." M"
            else
                xy_to_loc 
                ps swap 
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
    quit ;