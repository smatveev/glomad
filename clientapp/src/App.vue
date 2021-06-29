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

.mask {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    width: 100%;
    height: 100%;
    overflow: hidden;
    background-attachment: fixed;
}


.gradient-custom {
  /* fallback for old browsers */
  background: #1e3c72;

  /* Chrome 10-25, Safari 5.1-6 */
  background: -webkit-linear-gradient(to top, #1e3c72 0%, #1e3c72 1%, #2a5298 100%);

  /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
  background: linear-gradient(to top, #1e3c72 0%, #1e3c72 1%, #2a5298 100%);
}
</style>
