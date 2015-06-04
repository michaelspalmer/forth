\ constants will not change - more for readabilitly than anything
0 constant REJECT
1 constant SMALL
2 constant MEDIUM
3 constant LARGE
4 constant EXTRA-LARGE
5 constant ERROR

\ declare an array named counts with 6 slots
variable COUNTS 5 cells allot
\ zero fill all slots (can also use COUNTS 6 CELLS ERASE)
COUNTS 6 cells 0 fill

\ to get a value at an index, use ( n CELLS + ) as in (counts 1 cells +)
\ to get the 1 index value.
\ it works like this - 
\       COUNTS will put the address of the first slot on the stack
\       COUNTS . will do the same, but pull it off after
\       n CELLS is the size of n CELLS
\       if you start with the first slot
\               COUNTS
\       and add two cell values two it
\           COUNTS 2 CELLS +
\       you put the address of the third slot on the stack.
\           123 COUNTS 2 CELLS + !
\       will put 123 on the stack, get the address of the third slot, then
\       assign (!) the value to the slot.
: RESET COUNTS 6 cells erase ;

\ when we define a word to return an address (when given a slot number) we do it
\ backwards from above so that the passed-in slot number is the number of 
\ cells to add to counts - it will return the first slot address of COUNTS plus
\ n number of cells
: COUNTER cells COUNTS + ;

\ given that we need to put the slot value on the stack before the COUNTER word,
\ when we want to have it incremented by one, we first need the slot number on
\ the stack, then counter takes it off, puts back the address number we want,
\ then 1 gets pushed on, but for the increment world (+1), the definition is
\ ( n addr -- ), while we have ( slot_addr 1 -- ), so we swap them (1 slot_addr -- )
\ and now the increment word will add 1 to the slot address.
: TALLY COUNTER 1 swap +! ;

\ take in a weight - duplicate it onto the stack, then put each value on and compare it
\ if one passes, move to the "then", which moves to NIP.
\ NIP removes one item below the top of the stack (needs at least two on the stack)
\ example: ( 1 2 3 4 ) --NIP-> ( 1 2 4 ) --NIP-> ( 1 4 ) --NIP-> ERROR UNDERFLOW!
: CATEGORY ( weight -- category )
        DUP 18 < IF REJECT      ELSE
        DUP 21 < IF SMALL       ELSE
        DUP 24 < IF MEDIUM      ELSE
        DUP 27 < IF LARGE       ELSE
        DUP 30 < IF EXTRA-LARGE ELSE
    ERROR
    THEN THEN THEN THEN THEN NIP ;

\ remember that the constants listed at the top are really just integers from 0 to 5
\ so the LABEL word is just checking values of integers and returning a string.
: LABEL ( category -- )
        CASE
    REJECT      OF ." reject "      ENDOF
    SMALL       OF ." small "       ENDOF
    MEDIUM      OF ." medium "      ENDOF
    LARGE       OF ." large "       ENDOF
    EXTRA-LARGE OF ." extra large " ENDOF
    ERROR       OF ." error "       ENDOF
        ENDCASE ;

\ given a weight, pass it to CATEGORY, puts the associated number back on the stack
\ then duplicate it, pass one to label (to print it) the other to tally to count it
: EGGSIZE CATEGORY dup LABEL TALLY ;

\ the report is just a printing of all of them eventually
\ remember that the i is the do loop count, 5 u.r is just print with 5 spaces
\ also i COUNTER @ 5 U.R means get the value at the i slot and put it on the stack
\ then print it with five spaces. @ and ? are related, ? pulls the number off the stack
\ for you, @ just leaves it there.
: REPORT ( -- )
    PAGE
    ." QUANTITY     SIZE" cr cr
    6 0 
    do
        i COUNTER @ 5 u.r
        7 SPACES
        i LABEL cr
    loop ;