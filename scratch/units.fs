include random.fs
30 CONSTANT WIDTH
10 CONSTANT HEIGHT
create WORLD WIDTH HEIGHT * CELLS ALLOT
\ Convert to millimeters
\ OLD VERSION

\ : INCHES ( n1 -- n2 )
\     254 10 */ ;
\ : FEET  ( n1 -- n2 )
\     [ 254 12 * ] LITERAL 10 */ ;
\ there doesnt seem to be any difference
\ between feet and feet2: [ just moves to interpret mode, ] back out
\ : FEET2 ( n1 -- n2 )
\     254 12 * 10 */ ;
\ : YARDS  ( n1 -- n2 )
\     [ 254 36 * ] LITERAL 10 */ ;
\ : CENTIMETERS  ( n1 -- n2 )
\     10 * ;
\ : METERS  ( n1 -- n2 )
\     1000 * ;

: D, ( hi lo -- ) 
    SWAP , , ;
: D@ ( addr -- hi lo )  
    DUP @ SWAP CELL+ @ ;
: UNITS
    CREATE D, DOES> D@ */ ;

\ UNITS calls create, which uses D,, which wants two numbers (hi, lo), 
\ when called, UNITS 
254 10 UNITS INCHES
254 12 * 10 UNITS FEET
254 36 * 10 UNITS YARDS
10 1 UNITS CENTIMETERS
1000 1 UNITS METERS

: get_two_numbers ( n1 n2 -- )
    , , ;
: get_those_numbers_and ( addr -- n1 n2 )
    dup @ swap cell+ @ ;
: add_them_together
    + ;
: adding_these_together_makes
    CREATE get_two_numbers 
    DOES> get_those_numbers_and add_them_together ;


: Four, ( n1 n2 n3 n4 -- )
    , , , , ;
: generate_animal
    CREATE Four, ;

: rng_8 ( -- rn )
    8 random 1+ ;
: 8Random ( -- 8 rns )
    8 0 do rng_8 loop ;

: Eight, ( 8 rns -- compiled )
    , , , , , , , , ;
: generate_genes
    8Random CREATE Eight, ;

: xy_to_loc ( x y -- loc )
    swap WIDTH * + ;

: center
    width 2 / height 2 / xy_to_loc ;
    
