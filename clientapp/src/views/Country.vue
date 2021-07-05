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

<section class="container -py-4 my-5">
  <h2 class="fw-bold text-center">🏢 List of {{country.name}} enbassies</h2>

  <div class="py-2" v-for="e in embassy" :key="e.id">
    <div class="p-3 bg-light rounded-3">
      <div class="container-fluid">
        <h5 class="fw-bold">County {{ e.countryIata }}</h5>
        <p class="col-md-12 fs-6">📍 {{ e.address }}</p>
        <p class="col-md-12 fs-6">⏱ {{ e.workingHours }}</p>
        <p class="col-md-12 fs-6">☎ {{ e.phone }}</p>
        <p class="col-md-12 fs-6">📧 {{ e.email }}</p>
        <a :href="e.url" class="col-md-12 fs-6">{{ e.url }}</a>
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
