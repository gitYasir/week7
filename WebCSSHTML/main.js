window.alert("Good morning!")
document.write("Oh, and if I don't see ya")
console.log("Good afternoon, good evening and good night")

let height = 24
const INCH_TO_CM = 2.54

const character = []
character[0] = "Truman"
character.push("Harry")

const actor = {}
actor.firstName = "Jim"
actor.lastName = "Carey"
actor.getFullName = function () { return this.firstName + " " + this.lastName }

console.log(actor.getFullName())    //"Jim Carey")


function greeting() {
    window.alert("Good morning!")
    document.write("And if I don't see ya")
    console.log("Good afternoon, good evening and good night")
}

function changeText() {
    let para = document.createElement("p")
    let textNode = document.createTextNode(actor.getFullName())
    para.appendChild(textNode)
    document.getElementById("title-container").appendChild(para)
}