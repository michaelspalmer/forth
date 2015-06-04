\       page 
\ will clear the screen

\       clearstack 
\ will clear the stack

\       s" <filename>" included 
\ will load a file

\ so will 
\       load <filename>

\ and 
\       use <filename>

\ see <word> will show a word's definition

\ STACK should look like this: (x y z)
\ numbers are n, results are r1, r2, r3...

\       COMMAND     RETURNS     STACK
\           n                   (n)
\           m                   (n m)
\           o                   (n m o)
\           .           o       (n m)
\           .           m       (n)
\         m o                   (n m o)
\         SWAP                  (n m o)



\ built like this: (implicit)     z --   --> (z)
\                                 y --   --> (z y)
\                                 x --   --> (z y x)
\
\ and popped like:          (z y x) -- . --> x (z y)
\                           (y z)   -- . --> y (z)
\                           (z)     -- . --> z ()
\
\ ROT  move 3 to 1          (z y x) --ROT --> (x z y)
\ SWAP switch 1 and 2       (z y x) --SWAP--> (z x y)
\ DUP  duplicate 1          (z y x) --DUP --> (z y x x)




\ large letter F 

: star      42 emit ;
: stars     0 do star loop ;
: margin    cr 30 spaces ;
: blip      margin star ;
: bar       margin 5 stars ;
: F         bar blip bar blip blip cr ;

\ divisible by 10? 
\ push n onto stack                                   (n)
\ push 10 onto stack                                  (10 n)
\ pop last two values, divide them, 
\ push the remainder back onto the stack              (r1)
\ pop the last value, compare it to zero
\ if it is equal, push -1 if not push 0               (r2)
: ?div10 10 MOD 0= ;

\ days over 31 
\ push n onto the stack                               (n)
\ duplicate it                                        (n n)
\ push 1 onto the stack                               (1 n n)
\ compare the last two values, pushing the result     (r1 n)
\ swap the values on the stack                        (n r1)
\ push 31 onto the stack                              (31 n r1)
\ compare the last two values, pushing the result     (r2 r1)
\ add the results together, push result to stack      (r3)
\ if the result is true, meaning either
\ of the two compares was true (1 < or 31 >), 
\ return "NO WAY"
\ otherwise (compares passed), return LOOKS GOOD
\ then dont do anything
: ?day ( n --  )
    dup 1 < swap 31 > +
        if ." NO WAY!" else ." LOOKS GOOD!" then ;
    
\ boxtest ( l w h -- )
\ the definition above means that it takes the last three
\ values from the stack, leaving none behind
\ push three values onto the stack                    (h w l)
\ push 6 onto the stack                               (6 h w l)
\ pop the last two and compare them, push result      (r1 w l)
\ rotate 3 stack item to top                          (w r1 l)
\ push 22 onto the stack                              (22 w r1 l)
\ pop the last two and compare them, push result      (r2 r1 l)
\ rotate 3 stack item to top                          (l r2 r1)
\ push 19 onto the stack                              (19 l r2 r1)
\ pop the last two and compare them, push result      (r3 r2 r1)
\ boolean and for r3 r2, push result                  (r4 r1)
\ boolean and for r4 r1, push result                  (r5)
\ is this value not zero? print "big enough"
\ otherwise do nothing
: boxtest ( l w h --  )
    6 > ROT 22 > ROT 19 > AND AND
    IF ." Big Enough. " THEN ;


\ LOOPING
\ do and ?do same but ?do checks that start and end are not the same
\ this is a compile only word - it has to be part of a word's definition
\ (if is the same way)
\ myloop takes two numbers off the stack end and start
\ so 10 0 myloop will push 10 then 0                (0 10)
\ then pop for the start number first
\ and the end number second
\ this is confusing, but think of how it will be ordered
\ first push 10, then push 0, then pop 0 for the start number
\ then pop 10 for the end number
\ index with the implicit i variable (or r@ for reading the return stack)
\ loop can be changed to n +loop or n -loop where n is a step
\ there is also indefinite loops - learn about them
\ the syntax is:
\       begin <do something> <flag> until
: myloop   ( n n --  ) do cr ." hello" loop ;
: my1+loop ( n n --  ) do cr i . 1 +loop ;
: my2+loop ( n n --  ) do cr i . 2 +loop ;
: my3-loop ( n n --  ) 
    do 
        cr i . 
    3 -loop ;

\ a quick loop that will print a rectangle of differnt sizes
: rectangle ( n --  ) 0
    do 
        i 16 mod 0=    ( check if i is divisible by 16 )
        if 
            cr          ( if it is, newline )
        then 
            ." *"       ( always print a star )
    loop ;

\ write a little poem
: poem 
    cr 11 1
    do
        i . ." little "
        i 3 MOD 0=
        if
            ." indians " cr
        then
    loop
    ." inidian boys." ;
    
\ make a cool multiplication table
: table 
    cr 11 1 
    do
        cr 11 1 
        do 
            i j * 5 u.r
        loop cr
    loop ;
    
\ factorial
: fac 1 swap 1 max 1+ 2 ?do i * loop ;

\ factorial loop
: facloop 0 do i fac . loop cr ;


: doubled
    6 1000 21 1                                 \ <4> 6 1000 21 1
    do                                          \ <2> 6 1000
        cr                                      \ newline
        ." Year" i 2 u.r                        \ print i -> 2 u.r is spacing
        2dup r@ + dup                                   ( 1000 6 1000 6 )
        ."    Balance " .
        dup 2000 > if 
                        cr cr 
                        ." more than doubled in " 
                        i . ." years" 
                        leave
                    then
    loop 
    2drop ;
    

\ these are from animal.fs - they are here so they dont get lost, but theyll
\ probably be lost. . .
: pop_ani ( [animal] -- animal )
    animal_counter @ get_ani 
    0 animals animal_counter @ cells + ! ;
    
: pop_plt ( [plant] -- plant )
    plant_counter @ get_plt
    0 plants plant_counter @ cells + ! ;