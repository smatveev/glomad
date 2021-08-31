<template>

<!-- <div style="z-index: 10;" class="position-fixed start-0 bottom-0 p-2 m-1 bg-light text-dark border">
    By <a href="https://twitter.com/smatveev">@smatveev</a>
</div> -->
  <!-- <div class="home">
    <img alt="Vue logo" src="../assets/logo.png"> -->
    <HelloWorld msg="Welcome to Your Vue.js App"/>

    <footer class="container pt-5 border-top">
    <p class="float-end"><a href="#">Back to top</a></p>
    <p>Countries</p>
    <ul style="list-style-type:none; padding: 0;">
      <li><a href="Thailand">Thailand</a></li>
      <li><a href="Mexico">Mexico</a></li>
      <li><a href="Indonesia">Indonesia</a></li>
      <li><b><a href="Countries">All countries</a> 👉</b></li>
    </ul>

  </footer>

  <!-- </div> -->
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'Home',
  components: {
    HelloWorld
  },
  mounted() {
    document.title = "Be Glomad.net"
  }
}
</script>
