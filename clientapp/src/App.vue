<template>
  <!-- <div id="nav">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link>
  </div> -->
  
  <router-view/>
</template>

<style>
/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
} */

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.shadowed {
  text-shadow: 1px 1px 0 rgb(0 0 0 / 35%), 1px 1px 5px rgb(0 0 0 / 50%);
}

.preparing {
  background-color: black;
  opacity: .555;
}

.badge-card {
  z-index: 1;
}




/* Carousel styling */
        

        .carousel-item:nth-child(1) {
          background-image: url('/bg-1.jpg');
          background-repeat: no-repeat;
          background-size: cover;
          background-position: center center;
        }
        .carousel-item:nth-child(2) {
          background-image: url('https://mdbootstrap.com/img/Photos/Others/images/77.jpg');
          background-repeat: no-repeat;
          background-size: cover;
          background-position: center center;
        }
        .carousel-item:nth-child(3) {
          background-image: url('https://mdbootstrap.com/img/Photos/Others/images/78.jpg');
          background-repeat: no-repeat;
          background-size: cover;
          background-position: center center;
        }


</style>
