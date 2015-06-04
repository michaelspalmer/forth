\ for a simple word that takes one value from the stack:
: simple ( n -- )
    0 do i 3 u.r cr loop ;

\ tick gives us the execution token (xt)
\ looks like an address (might be?!)
\ ' simple
\ so execute will perfore this (this is what the interpreter does)
\ 10 ' simple execute ( is the same as just simple )

\ use it to find if a word has been defined without actually calling it
\ ' simple . ( returns an error if there isnt a value )

\ dump is a built in word, and can show the contents of the definition
\ ' simple 12 cells dump

\ vectored definition means setting the xt to a variable and executing that
\ the advantage here is that we can change it later
variable 'simple ' simple 'simple !
\ pay attention - that one line just defined the variable, then set the xt of
\ simple to it.

\ now if we define 
: TEN 5 5 + ;
: FIVE 3 2 + ;

\ and define a new variable that is the xt of those words
variable 'lup 

\ set it to the xt of one of them
' TEN 'lup !

\ then make a word to call it
: luup 'lup @ execute ;

\ these two new words will then change them as needed, and use them for the simple loop
: 10loop ['] TEN  'lup ! luup simple ;
: 5loop  ['] FIVE 'lup ! luup simple ;

\ while this is a small example, it makes a point.  though 15loop could just be
: 15loop 5 5 5 + + simple ;

