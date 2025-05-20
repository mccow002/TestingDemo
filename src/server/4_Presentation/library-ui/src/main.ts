import {bootstrapApplication} from "@angular/platform-browser";
import {provideHttpClient, withInterceptorsFromDi} from "@angular/common/http";
import {AppComponent} from "./app/app.component";
import {provideAnimations} from "@angular/platform-browser/animations";
import {provideRouter} from "@angular/router";
import {routes} from "./app/routes";
import {provideToastr} from "ngx-toastr";
import {importProvidersFrom, inject, provideAppInitializer} from "@angular/core";
import {ModalModule} from "ngx-bootstrap/modal";
import {URL_TOKEN} from "./app/shared/models";
import {SignalrService} from "./app/shared/signalr.service";
import {provideStore} from "@ngrx/store";


bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideHttpClient(withInterceptorsFromDi()),
    provideRouter(routes),
    provideStore(),
    provideToastr(),
    importProvidersFrom(ModalModule.forRoot()),
    {
      provide: URL_TOKEN,
      useValue: 'api'
    },
    provideAppInitializer(() => {
      const hub = inject(SignalrService);
      return hub.start()
    })
  ]
}).catch(error => console.error(error));
