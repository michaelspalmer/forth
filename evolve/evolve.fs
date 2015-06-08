include header.fs

reset_all

^gen gs gs 0 500 center ^ani an an as >>ani

\ : evolve


\ NOTES:


\ false = 0
\ true  = anything else
\ x y WIDTH * +  will give the index
\ C code for this:
\
\ int array[width * height];
\
\ int SetElement(int row, int col, int value)
\ {
\    array[width * row + col] = value;
\    array[width * y + x ]
\ }



\ WORD Name Reference:
\ ^  -- used when generating structures
\ @  -- puts the address on the stack
\ /  -- actions (a/eat a/move)
\ ?  -- prints the value at the address
\ >  -- sets the value at the address
\ +  -- increment something
\ -  -- decrement something
\ >> -- push onto the array
\ << -- pop from the array
\
\ EXAMPLE USAGE:
\ >>n ( n [n] -- ) Given an n and an array of n, push n onto [n], return nothing
\ @x  ( n -- x ) Given an n, return the address to that n's x value to the stack
