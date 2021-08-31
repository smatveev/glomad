

<template>
  <div class="hero-bg px-4 py-5 text-white text-center">
    <h1 class="display-5 fw-bold">{{ country.name }}</h1>
    <h5 class="my-3">⏱ Updated {{ dateTime(country.updateDate) }}</h5>

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

  <Tabs :selec="this.hash.substring(1)">

<Tab title="Visas">
      <h2 class="fw-bold text-center p-4">💳 List of {{country.name}} visa types</h2>

<div class="row row-cols-sm-1  row-cols-lg-3 row-cols-md-2 g-4">
<div class="py-2" v-for="v in visas" :key="v.id">
      <div class="p-3 bg-light rounded-3">
        <div class="container-fluid">
          <h5 class="fw-bold">{{ v.visaName }}</h5>

          <p class="col-md-12">{{ v.description }}</p>

          <span
            v-if="v.isExdendable"
            class="badge badge-card bg-info text-dark m-2 fs-6"
          >
            ✨ Extendable
          </span>

          <span class="badge badge-card bg-info text-dark m-2 fs-6">
            🕜 Duration {{ v.duration }} days
          </span>
        </div>
      </div>
    </div>

    <div class="p-3 bg-light rounded-3">
        <W/>
      </div>
</div>
    

    </Tab>

    <Tab title="Embassies">
      <h2 class="fw-bold text-center p-4">🏢 List of {{country.name}} embassies</h2>

      <div class="row row-cols-2 row-cols-md-3 row-cols-lg-4 g-4">
      <div class="col" v-for="e in embassy" :key="e.id">
        <div class="card rouded-3">
          <div class="card-body">
            <flag :iso="e.iata" /> 
            <a :href="country.name + '/Embassy/' + e.id" class="card-link m-2">{{e.country}}, {{ e.city }}</a>
          </div>
        </div>
      </div>
      </div>
    </Tab>

    
    <Tab title="Covid">
      <!-- COVID restrictions -->
        <section v-if="covidInfo">
          <!-- <h1 class="display-5 fw-bold">Proven countries</h1> -->
          <h2 class="fw-bold text-center p-4">🦠 {{country.name}} COVID-19 travel requirements</h2>

          <p class="p-3" v-html="covidInfo"></p>
        </section>
        <!-- -- COVID restrictions -->
    </Tab>
  </Tabs>  
  
</section>
  
 
</template>

<script>

import Tabs from '@/components/Tabs.vue'
import Tab from '@/components/Tab.vue'
import moment from 'moment'

import W from '@/components/Widget.vue'

export default {
  //props: ['id'],
  data() {
    return {
      country: {},
      name: this.$route.params.name,
      hash: this.$route.hash,
      embassy: {},
      
      visasNonEntry: {},
      visas: {},
      covidInfo: ""
    };
  },
  created() {
    //alert(this.$route.params.name)
  },
  mounted() {
    document.title = this.$route.params.name + " travel COVID-19 requirements - Glomad"

    fetch(process.env.VUE_APP_API_URL + "Countries/GetByName?name=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.country = data;
        console.info(data);
      })
      .catch((err) => console.log(err.message));

    fetch(process.env.VUE_APP_API_URL + "Countries/GetCountryEmbassies?country=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.embassy = data;
        console.info(data);
      })
      .catch((err) => {
        console.warn(err.message);
        this.embassy = null;
      });


    fetch(process.env.VUE_APP_API_URL + "Visas?country=" + this.name)
        .then((res) => res.json())
        .then((data) => {
          this.visas = data;
          console.info(data);
        })
        .catch((err) => {
          console.warn(err.message);
          this.visas = [];
        });
    
    fetch(process.env.VUE_APP_API_URL + "Countries/GetCovidInfo?name=" + this.name)
        .then((res) => res.text())
        .then((text) => (this.covidInfo = text))
        .catch((err) => {
          console.warn(err.message);
        });
  },
  components: {
    Tabs,
    Tab,
    W
  },
  methods: {
    dateTime(value) {
      return moment(value, "YYYYMMDDHHmmss").fromNow(); 
      },
    foo(p) {
      alert(p)
    }
  }
};
</script>
