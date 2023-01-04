class Balloon {
    constructor() {
        this.x = random(width)
        this.y = random(height)
        this.r = 25
        this.vx = 0
        this.vy = 0
        this.colour = color(random(255), random(255), random(255))
        this.popped = false
    }
    blowAway() {
        if (!this.popped) {
            //calculate the blow away force
            let d = dist(this.x, this.y, mouseX, mouseY)
            let force = d > height / 2 ? 0 : -10 / (d * d)



            //apply the force to the existing velocity
            this.vx += force * (mouseX - this.x)
            this.vy += force * (mouseY - this.y)
        }



        //also add some friction to take energy out of the system
        this.vx *= 0.95
        this.vy *= 0.95



        //update the position
        this.x += this.vx
        this.y += this.vy
    }



    checkBounds() {
        //make balloon wrap to the other side of the canvas
        if (this.x < 0) this.x = width
        if (this.x > width) this.x = 0
        if (this.y < 0) this.y = height
        if (this.y > height) this.y = 0
    }
    checkToPop() {
        if (!this.popped && dist(this.x, this.y, mouseX, mouseY) < this.r) {
            this.popped = true
            let currScore = Number(document.getElementById("score").innerHTML)
            currScore++
            document.getElementById("score").innerHTML = currScore
            this.colour = color(156)
        }
    }
}



let balloons = []
const BALLOONTOTAL = 20



function setup() {
    let canvas = createCanvas(640, 480)
    canvas.parent("gameContainer")
    for (let i = 0; i < BALLOONTOTAL; i++) balloons.push(new Balloon())
}



function draw() {
    //a nice sky blue background
    background(135, 206, 235)



    for (let i = 0; i < BALLOONTOTAL; i++) {
        balloons[i].checkToPop()
        balloons[i].checkBounds()
        balloons[i].blowAway()
        fill(balloons[i].colour)
        circle(balloons[i].x, balloons[i].y, balloons[i].r)
    }
}