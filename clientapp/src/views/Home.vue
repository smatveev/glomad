<template>
<div style="z-index: 10;" class="position-fixed start-0 bottom-0 p-2 m-1 bg-light text-dark border">
    By <a href="https://twitter.com/smatveev">@smatveev</a> text me with your 💡
</div>
  <!-- <div class="home">
    <img alt="Vue logo" src="../assets/logo.png"> -->
    <HelloWorld msg="Welcome to Your Vue.js App"/>
  <!-- </div> -->
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'Home',
  components: {
    HelloWorld
  }
}
</script>
