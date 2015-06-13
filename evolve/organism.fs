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
    swap 2 cells + ! ;

\ get the address of the gene array
: @gen ( animal -- [n] )
    3 cells + @ ;
\ get the gene at the given index
: idx@gen ( animal index -- n )
    swap @gen swap cells + @ ;
\ loop through and print genes - for debug
: gen_lp ( animal -- )
    8 0
    do
        \ dup i idx@gen NOT WORKING the r@ holds same as i . . .
    loop
    ;

\ push to array
: rev3 ( n1 n2 n3 -- n3 n2 n1 )
    swap rot ;
    
: >>ani ( animal [animal] -- )
    over @loc cells + ! ;
    
: >>plt ( loc [plants] -- )
    1 rev3 cells + ! ;
    
: >>rndplt ( -- )
    1 PLANTS rndloc cells + ! ;

: a/eat ( a -- )
    dup >r
    @loc hasplant?
    r>
    swap 0
    or
    if
		dup
        PE >enr
		0 swap PLANTS swap @loc cells + !
    else
        drop
    then ;

: get_new_xy ( dir -- x y )
    >r              ( ) \ r( dir )
    r@ 0 >          ( t|f )
    r@ 4 <          ( t|f t|f )
    AND             ( t|f )         \ if both are true
    if
        WIDTH                       \ +x means add WIDTH amount
    else
        r@ 4 >      ( t|f)
        r@ 8 <      ( t|f t|f )
        AND         ( t|f )
        if
            -1 WIDTH *              \ -x means subtract WIDTH amount
        else
            0                       \ then it's on the axis - return 0
        then
    then
    r@ 2 > 
    r@ 6 < 
    AND
    if
        1
    else
        r@ 7 =      ( t|f )
        r@ 0 >=     ( t|f t|f )
        r@ 2 <      ( t|f t|f t|f )
        AND         ( t|f t|f )
        OR          ( t|f )
        if
            -1
        else
            0
        then
    then
    r> drop
    ;

: one_more ( a -- nloc )
    dup @loc swap @dir  ( loc dir )
    get_new_xy          ( loc x y )
    rot + +             ( nloc )
    ;

: a/move ( a --  )
    dup >r              ( a )        \ r( a )
    one_more            ( nloc )     \ r( a )
    dup hasanimal?      ( nloc n )   \ r( a )
    0 and
    0 or
    if
        r>              ( nloc a )
        dup dup         ( nloc  a a a )
        @dir 1+ 
        8 MOD >dir      ( nloc a )
        swap drop       ( a )           \ leave one here for recursion
        recurse                         \ what if there are no more moves?
    else                ( nloc )
        0               ( nloc 0 )     
        r> dup rot      ( nloc a a 0 )
        animals         ( nloc a a 0 as )
        rot             ( nloc a 0 as a )
        @loc cells + !  ( nloc a )      \ set old location to 0
        dup             ( nloc a a )
        rot             ( a a nloc )
        LENGTH MOD
        >loc            ( a )           \ set new loc on animal
        dup -1 >enr
        animals >>ani   ( )             \ push animal to array
    then
    ;
    
: a/turn ( a -- )
    dup dup         ( a a a )
    @dir >r         ( a a )     \ r( dir )
    3 idx@gen r@ >  ( a t|f )   \ r( dir )
    if
        r@ 1+ >dir  ( )         \ r( dir )
    else
        r@ 1- >dir  ( )         \ r( dir )
    then
    r> drop
    ;
    
: a/dead? ( a -- bool )
    @enr 1 < ;

: simulate_day ( -- )
    >>rndplt
    length 0
    do
        i hasanimal?
        0 OR
        if
            animals i @idx a/turn
            animals i @idx a/eat
            animals i @idx a/dead?
            if
                0 animals i cells + !
                leave
            then
            animals i @idx a/move
        then
    loop
    show_world
    ;

: simulate_time ( -- )
    begin
        simulate_day
    300 ms
    show_world
    1 0 =
    until
    ;

\ locate active animal - only works when one (will only find the first if more)
: find_ani ( debug )
    LENGTH 0 
    do 
        animals i 
        @idx 
        dup dup 0= 
        swap 1 =
        or
        if 
            drop 
        else 
            @ leave 
        then 
    loop ;

\ debug animal
: ani animals find_ani cells + @ ;

\ test of recursion for animal movement
: rec-test ( debug )
    cr
    ani 
    dup >r 
    one_more 
    dup hasanimal? 
    if 
        r> 
        dup dup 
        @dir 1+ >dir 
        swap drop drop 
        ." added one to dir, recursing ... "
        cr
        recurse 
    else 
        r> 
        drop drop 
        ." no animal here!" 
    then ;