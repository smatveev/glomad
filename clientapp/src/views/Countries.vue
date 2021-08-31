<template>
    <h2 class="fw-bold text-center p-4">All countries</h2>

      <div class="row row-cols-2 row-cols-md-3 row-cols-lg-4 g-4">
      <div class="col" v-for="c in countries" :key="c.id">
        <div class="">
          <div class="card-body">
            <flag :iso="c.iata" /> 
            <a :href="c.name" class="card-link m-2">{{c.name}}, ⏱ Updated {{ dateTime(c.updateDate) }}</a>
          </div>
        </div>
      </div>
      </div>
</template>


<script> 

import moment from 'moment'

export default {
  data() {
    return {
      countries: []
    };
  },
  mounted() {
    document.title = "All countries with actial Covid and travel data - Glomad.net"
    
    fetch(process.env.VUE_APP_API_URL + "Countries/GetUpdatedCountries")
      .then((res) => res.json())
      .then((data) => {
        this.countries = data;
        console.info(data);
      })
      .catch((err) => console.log(err.message));
  },
    methods: {
    dateTime(value) {
      return moment(value, "YYYYMMDDHHmmss").fromNow(); 
      },
    }
};
</script>