

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

<ul class="nav nav-tabs nav-justified">
      <li class="nav-item">
        <a class="nav-link" @click="setActive('Visas')" :class="{ active: isActive('Visas') }" :href="'#Visas'">🎫 Visas</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" @click="setActive('Embassies')" :class="{ active: isActive('Embassies') }" :href="'#Embassies'">🏤 Embassies</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" @click="setActive('Covid')" :class="{ active: isActive('Covid') }" :href="'#Covid'">😷 Covid</a>
      </li>
</ul>

    <div class="tab-content py-3" id="myTabContent">
      <div class="tab-pane fade" :class="{ 'active show': isActive('Visas') }" id="Visas">
            <h2 class="fw-bold text-center p-4">💳 List of {{country.name}} visa types</h2>

      <div class="row row-cols-sm-1  row-cols-lg-3 row-cols-md-2 g-4">
        <div v-for="v in visas" :key="v.id">
              <div class="p-3 m-1 bg-light rounded-3">
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

        <div class="p-2 bg-light rounded-3">
          <WC v-if="country.capitalCode" :to="country.capitalCode" />
        </div>


    </div>
      </div>

      <div class="tab-pane fade" :class="{ 'active show': isActive('Embassies') }" id="Embassies">
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
        </div>

      <div class="tab-pane fade" :class="{ 'active show': isActive('Covid') }" id="Covid">
                <section v-if="covidInfo">
          <!-- <h1 class="display-5 fw-bold">Proven countries</h1> -->
          <h2 class="fw-bold text-center p-4">🦠 {{country.name}} COVID-19 travel requirements</h2>

          <p class="p-3" v-html="covidInfo"></p>

        </section>

        </div>
    </div>

  <!-- <Tabs :selec="this.hash.substring(1)">

<Tab title="Visas" v-on:click="foo('visas')"> -->
    <!-- </Tab> -->

    <!-- <Tab title="Embassies">
      
    </Tab> -->

    
    <!-- <Tab title="Covid">

    </Tab>
  </Tabs>   -->
  
</section>
  
 
</template>

<script>
// import Tabs from '@/components/Tabs.vue'
// import Tab from '@/components/Tab.vue'
import moment from "moment";

import WC from "@/components/WidgetCountry.vue";

export default {
  //props: ['id'],
  data() {
    return {
      activeItem: "",
      country: {},
      name: this.$route.params.name,
      hash: this.$route.hash,
      embassy: {},

      visasNonEntry: {},
      visas: {},
      covidInfo: "",
    };
  },
  created() {
    //alert(this.$route.params.name)
    this.setActive(this.hash.substring(1) ? this.hash.substring(1) : "Visas");
  },
  mounted() {
    document.title =
      this.$route.params.name + " travel COVID-19 requirements - Glomad";

    fetch(process.env.VUE_APP_API_URL + "Countries/GetByName?name=" + this.name)
      .then((res) => res.json())
      .then((data) => {
        this.country = data;
        console.info(data);
      })
      .catch((err) => console.log(err.message));

    fetch(
      process.env.VUE_APP_API_URL +
        "Countries/GetCountryEmbassies?country=" +
        this.name
    )
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

    fetch(
      process.env.VUE_APP_API_URL + "Countries/GetCovidInfo?name=" + this.name
    )
      .then((res) => res.text())
      .then((text) => (this.covidInfo = text))
      .catch((err) => {
        console.warn(err.message);
      });
  },
  components: {
    // Tabs,
    // Tab,
    WC,
  },
  methods: {
    dateTime(value) {
      return moment(value, "YYYYMMDDHHmmss").fromNow();
    },
    foo(p) {
      alert(p);
    },
    isActive(menuItem) {
      return this.activeItem === menuItem;
    },
    setActive(menuItem) {
      this.activeItem = menuItem;
    },
  },
};
</script>
