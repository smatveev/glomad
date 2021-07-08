<template>
  <div class="hero-bg px-4 py-5 text-white text-center">
    <h1 class="display-5 fw-bold">{{ country.name }}</h1>
    <h5 class="my-3">⏱ updated 3 min ago</h5>

    <div class="col-lg-6 mx-auto position-absolute">
        <p class="lead mb-4">
        <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><a href="/">Home</a></li>
          <li class="breadcrumb-item active" aria-current="page">{{ country.name }}</li>
        </ol>
      </nav>
      </p>
      
    </div>
  </div>

<section class="container py-3">
  <h2 class="fw-bold text-center p-4">🏢 List of {{country.name}} embassies</h2>

<div class="row row-cols-2 row-cols-md-3 row-cols-lg-4 g-4">
      <div class="col" v-for="e in embassy" :key="e.id">
        <div class="card rouded-3">
          <div class="card-body">
            <h5 class="card-title">{{e.countryIata}}</h5>
            <p class="card-text">📍 {{ e.address }}</p>
            <a href="#" class="card-link">Details</a>
          </div>
        </div>
    </div>
</div>
  
</section>
  
 
</template>

<script>
export default {
  //props: ['id'],
  data() {
    return {
      country: {},
      name: this.$route.params.name,
      embassy: {},
    };
  },
  mounted() {
    fetch(process.env.VUE_APP_API_URL + "Countries/GetByName?name=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.country = data;
        console.info(data);
      })
      .catch((err) => console.log(err.message));

    fetch(process.env.VUE_APP_API_URL + "Countries/GetCountryEmbassies")
      .then((res) => res.json())
      .then((data) => {
        this.embassy = data;
        console.info(data);
      })
      .catch((err) => {
        console.warn(err.message);
        this.embassy = null;
      });
  },
};
</script>
