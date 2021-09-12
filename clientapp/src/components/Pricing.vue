<template>

<Dialog v-model:show="showPlan1">
      <div class="container">
        <div class="py-2 row">
          <h3 class="pb-3 text-center">Your selected plan is "One country" - $10</h3>
          <div class="col-sm-12 col-lg-8">
              <p>😷 COVID-19 travel restriction in selected countries.</p>
              <p>🎫 The range of visa opportunities for digital nomads.</p>
              <p>🛂 The longest duration of stay according to visa rules. </p>
              <p>👌 The easiest way to apply for a visa.</p>
              <p>✅ Checklist of documents to apply for a visa.</p>
              <p>⌛ Is your visa extendable? Extension conditions.</p>
              <p>✈ Avia tickets costs. </p>
              <p>🌅 Climatic conditions.</p>
              <p>💲 Minimum cost of living.</p>
          </div>
            <div class="col-sm-12 col-lg-4">
              <p>You'll get all this information about one country. Please, just send us interested country's name.</p>
              <form>
                <div class="form-floating mb-3">
                  <input
                    type="email"
                    class="form-control"
                    placeholder="example: name@example.com"
                  />
                  <label>Type your email</label>
                </div>
                <div class="form-floating mb-3">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Your name"
                  />
                  <label>Your name</label>
                </div>
                <div class="form-floating mb-3">
                  <textarea
                    type="text"
                    class="form-control"
                    placeholder="Please, provide country name"
                  />
                  <label>Details</label>
                </div>

        <button class="w-100 btn btn-lg btn-primary" type="submit">
          Thank you ✌
        </button>

      </form>
            </div>
        </div>
        <p class="text-center">From nomads for nomads with ❤.</p>
      </div>
    </Dialog>

<Dialog v-model:show="showPlan2">
      <div class="container">
        <div class="py-2 row">
          <h3 class="pb-3 text-center">Your selected plan is "Five countries"  - $25</h3>
          <div class="col-sm-12 col-lg-8">
              <p>😷 COVID-19 travel restriction in selected countries.</p>
              <p>🎫 The range of visa opportunities for digital nomads.</p>
              <p>🛂 The longest duration of stay according to visa rules. </p>
              <p>👌 The easiest way to apply for a visa.</p>
              <p>✅ Checklist of documents to apply for a visa.</p>
              <p>⌛ Is your visa extendable? Extension conditions.</p>
              <p>✈ Avia tickets costs. </p>
              <p>🌅 Climatic conditions.</p>
              <p>💲 Minimum cost of living.</p>
          </div>
            <div class="col-sm-12 col-lg-4">
              <p>You'll get all this information about five selected countries. Please, just send us five names of the countries.</p>
              <form>
                <div class="form-floating mb-3">
                  <input
                    type="email"
                    class="form-control"
                    placeholder="example: name@example.com"
                  />
                  <label>Type your email</label>
                </div>
                <div class="form-floating mb-3">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Your name"
                  />
                  <label>Your name</label>
                </div>
                <div class="form-floating mb-3">
                  <textarea
                    type="text"
                    class="form-control"
                    placeholder="Please, provide country name"
                  />
                  <label>Details</label>
                </div>

        <button class="w-100 btn btn-lg btn-primary" type="submit">
          Thank you ✌
        </button>

      </form>
            </div>
        </div>
        <p class="text-center">From nomads for nomads with ❤.</p>
      </div>
    </Dialog>

    <Dialog v-model:show="showPlan3">
      <div class="container">
        <div class="py-2 row">
          <h3 class="pb-3 text-center">Your selected plan is "Best plan for your"  - $30</h3>
          <div class="col-sm-12 col-lg-8">
              <p>😷 COVID-19 travel restriction in selected countries.</p>
              <p>🎫 The range of visa opportunities for digital nomads.</p>
              <p>🛂 The longest duration of stay according to visa rules. </p>
              <p>👌 The easiest way to apply for a visa.</p>
              <p>✅ Checklist of documents to apply for a visa.</p>
              <p>⌛ Is your visa extendable? Extension conditions.</p>
              <p>✈ Avia tickets costs. </p>
              <p>🌅 Climatic conditions.</p>
              <p>💲 Minimum cost of living.</p>
          </div>
            <div class="col-sm-12 col-lg-4">
              <p>You'll get all this information about tree countries most sutable for you. 
                We'll check Covid-resrtrictions, visa conditions, prices and etc. Please, send us: 
                Where are you stay now?
                What nationality do you have?
                Approximately size of your budget?
                Please, tell us if  you have any wishes and preferences regarding the upcoming trip.
              </p>
              <form>
                <div class="form-floating mb-3">
                  <input
                    type="email"
                    class="form-control"
                    placeholder="example: name@example.com"
                  />
                  <label>Type your email</label>
                </div>
                <div class="form-floating mb-3">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Your name"
                  />
                  <label>Your name</label>
                </div>
                <div class="form-floating mb-3">
                  <textarea
                    type="text"
                    class="form-control"
                    placeholder="Please, provide country name"
                  />
                  <label>Details</label>
                </div>

        <button class="w-100 btn btn-lg btn-primary" type="submit">
          Thank you ✌
        </button>

      </form>
            </div>
        </div>
        <p class="text-center">From nomads for nomads with ❤.</p>
      </div>
    </Dialog>

    <div class="row row-cols-1 row-cols-md-3 mb-3 text-center">
      
      <div class="col">
        <div class="card mb-4 rounded-3 shadow-sm">
          <div class="card-header py-3">
            <h4 class="my-0 fw-normal">☝ One country</h4>
          </div>
          <div class="card-body">
            <h1 class="card-title pricing-card-title">$5<small class="text-muted fw-light">/one time</small></h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>🛂 Visa details</li>
              <li>🎫 Document list</li>
              <li>😷 Covid restrictions</li>
            </ul>
            <button type="button" class="w-100 btn btn-lg btn-primary" @click="dialogPlan1()">Get started</button>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card mb-4 rounded-3 shadow-sm">
          <div class="card-header py-3">
            <h4 class="my-0 fw-normal">🖐 Five countries</h4>
          </div>
          <div class="card-body">
            <h1 class="card-title pricing-card-title">$25<small class="text-muted fw-light">/one time</small></h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>🛂 Visa details</li>
              <li>🎫 Document list</li>
              <li>😷 Covid restrictions</li>
            </ul>
            <button type="button" class="w-100 btn btn-lg btn-primary" @click="dialogPlan2()">Get started</button>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card mb-4 rounded-3 shadow-sm border-primary">
          <div class="card-header py-3 text-white bg-primary border-primary">
            <h4 class="my-0 fw-normal">💪 Best plan for your</h4>
          </div>
          <div class="card-body">
            <h1 class="card-title pricing-card-title">$30<small class="text-muted fw-light">/one time</small></h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>Individualy prepare 3 destinaion points</li>
              <li>based on your current situation</li>
            </ul>
            <button type="button" class="w-100 btn btn-lg btn-primary" @click="dialogPlan3()">Get started</button>
          </div>
        </div>
      </div>
    </div>
</template>

<script>

import Dialog from '@/components/UI/Dialog.vue'

export default{
  data() {
    return {
      showPlan1: false,
      showPlan2: false,
      showPlan3: false,
    }
  },
  methods: {
    dialogPlan1() {
        this.showPlan1 = true;
    },
    dialogPlan2() {
        this.showPlan2 = true;
    },
    dialogPlan3() {
        this.showPlan3 = true;
    }
  },
  components: {
    Dialog
  }
}
</script>
