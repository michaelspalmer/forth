\ ----------------------------------------------------------------------- 
\ 8-6.FORTH ------------------------------------------------------------- 
\ ----------------------------------------------------------------------- 
\ Code from Starting Forth Chapter 8 
\ ANSized by Benjamin Hoyt in 1997 ( problem 8-6 ) 

CREATE BOARD 9 ALLOT 

: SQUARE ( square# -- addr ) BOARD + ; 

: CLEAR BOARD 9 ERASE ; 

CLEAR 

: BAR ." | " ; 

: DASHES CR 9 0 DO [CHAR] - EMIT LOOP CR ; 

: .BOX ( square# -- ) SQUARE C@ DUP 0= IF 2 SPACES ELSE DUP 1 = IF ." X " ELSE ." O " THEN THEN DROP ; 

: DISPLAY ( -- ) CR 9 0 DO I IF I 3 MOD 0= IF DASHES ELSE BAR THEN THEN I .BOX LOOP CR QUIT ; 

: PLAY ( player square# -- ) 1- 0 MAX 8 MIN SQUARE C! ; 


: X! ( square# -- ) 1 SWAP PLAY DISPLAY ; 
: O! ( square# -- ) -1 SWAP PLAY DISPLAY ; 