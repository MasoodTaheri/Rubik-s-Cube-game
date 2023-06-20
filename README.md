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




![Screenshot_4](https://github.com/MasoodTaheri/Rubik-s-Cube-game/assets/7465294/981af229-5091-402b-ab52-922d8bff429e)
![Screenshot_3](https://github.com/MasoodTaheri/Rubik-s-Cube-game/assets/7465294/ae71f248-6da5-448c-9a0e-bad7680bea76)
![Screenshot_2](https://github.com/MasoodTaheri/Rubik-s-Cube-game/assets/7465294/af2a1adb-0af1-40ac-8f35-d7708182c505)
![Screenshot_1](https://github.com/MasoodTaheri/Rubik-s-Cube-game/assets/7465294/b66d8749-2791-4ca0-a46a-888f1bab0fc4)



