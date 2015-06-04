DECIMAL

: star [char] * emit ;

: .row 
    8 0
    do 
        dup 128
        and
        if
            star
        else
            space
        then
        1 lshift
    loop
    drop ;
    
: shape 
    create 
    8 0 
        do 
            c, 
        loop
    does>
        dup 7 +
        do
            i c@ .row cr
        -1
        +loop
        cr 
        ;
        
HEX 
18 18 3C 5A 99 24 24 24 shape man
81 42 24 16 16 24 24 81 shape equis
AA AA FE FE 38 38 38 FE shape castle
DECIMAL