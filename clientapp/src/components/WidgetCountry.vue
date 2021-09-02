<template>
    <div ref="d"></div>
</template>


<script>

export default ({
    props: ['to'],
    mounted() {
      let script = document.createElement('script')
      script.setAttribute('src', "https://tp.media/content?currency=usd&promo_id=4044&shmarker=14510.glomad&campaign_id=100&trs=21652&target_host=search.jetradar.com&locale=en_us&limit=6&powered_by=false&destination=" + this.to)
      script.setAttribute('charset', "utf-8")
      this.$refs['d'].appendChild(script)

      console.log("widget country capital = " + this.to);
    },
 
})
</script>