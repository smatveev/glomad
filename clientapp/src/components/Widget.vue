<template>
    <div ref="d"></div>
</template>


<script>

export default ({
    mounted() {
      let script = document.createElement('script')
      script.setAttribute('src', "https://tp.media/content?promo_id=4041&shmarker=14510.glomad&campaign_id=100&trs=21652&searchUrl=search.jetradar.com&locale=en&currency=usd&powered_by=false&one_way=false&only_direct=false&period=year&range=7%2C14&primary=%230C73FE&color_background=%23ffffff&achieve=%2345AD35&dark=%23000000&light=%23FFFFFF&origin=MOW&destination=BKK")
      script.setAttribute('charset', "utf-8")
      this.$refs['d'].appendChild(script)
    },

})
</script>