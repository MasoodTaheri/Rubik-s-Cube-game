# Rubik-s-Cube-game
Rubik's Cube game

Implemented features:
-	Shuffle at start
-	Drag camera by mouse (left (out of cube) and right) smoothly
-	Rotate a cube around 3 axis (but just 2 of them applicable by mouse)
-	Camera zooms in and out
-	Result page (after 10 minutes or after success) with the Restart Button (MVP)


When player click on a cube:

1-	Detect that cube

2-	Detect 3 pivot and create a list of object for that pivot(that will rotate by that pivot)

3-	Detect the direction of mouse move

4-	Select the best pivot for rotation

5-	Rotate selected cubes around the pivot

6-	Check the timer for 10 minutes or solve the cube


The easier way to find the cube around a pivot was using lots of raycast but I used the position and a LINQ to find all the cubes that have same pivot.
Also, I find out that the solvation means having all cube in same rotation.




https://github.com/MasoodTaheri/Rubik-s-Cube-game/assets/7465294/bdd22957-9de6-48f0-8900-bd4d6946545c


