import { Routes } from '@angular/router';
import { Login } from './Features/Auth/login';
import { Loginlayout } from './layouts/loginlayout/loginlayout';
import { CrearUsuario } from './Features/crear-usuario/crear-usuario';
import { Mainlayout } from './layouts/mainlayout/mainlayout';
import { Home } from './Features/home/home';
import { CrearRutina } from './Features/Rutina/crear-rutina/crear-rutina';
import { ObtenerDiaRutina } from './Features/Rutina/Obtener-dia-rutina/obtener-dia-rutina';
import { CrearDiaRutina } from './Features/Rutina/crear-dia-rutina/crear-dia-rutina';
import { AgregarEjercicioDia } from './Features/Rutina/agregar-ejercicio-dia/agregar-ejercicio-dia';
import { ObtenerGruposMusculares } from './Features/Rutina/obtener-grupos-musculares/obtener-grupos-musculares';
import { ObtenerSubgruposMusculares } from './Features/Rutina/obtener-subgrupos-musculares/obtener-subgrupos-musculares';
import { EjerciciosSubgrupo } from './Features/Rutina/ejercicios-subgrupo/ejercicios-subgrupo';
import { CompletarDatosEjercicio } from './Features/Rutina/completar-datos-ejercicio/completar-datos-ejercicio';
import { SesionEntrenamiento } from './Features/Sesion/sesion-entrenamiento/sesion-entrenamiento';
import { EstadoUsuarioSesion } from './Features/Sesion/estado-usuario-sesion/estado-usuario-sesion';
import { RegistroSerie } from './Features/Sesion/registro-serie/registro-serie';
import { EditarMesociclo } from './Features/Rutina/editar-mesociclo/editar-mesociclo';
import { Graficos } from './Features/graficos/graficos';


export const routes: Routes = [
    {
        path:'auth',
        component:Loginlayout,
        children:[
            { path: 'login', component: Login },
            {path:'registrar',component:CrearUsuario}
        ]
    },
        {
        path:'',
        component:Mainlayout,
        children:[
            {path:'home',component: Home},
            {path:'nuevomesociclo',component:CrearRutina},
            {path:'diarutina/:uid', component: ObtenerDiaRutina},
            {path:'creardia/:uid',component:CrearDiaRutina},
            {path:'agregarejercicio/:uid',component:AgregarEjercicioDia},
            {path:'gruposmusculares',component:ObtenerGruposMusculares},
            {path:'subgruposmusculares/:idGrupo',component:ObtenerSubgruposMusculares},
            {path:'ejerciciossubgrupo/:idSubgrupo',component:EjerciciosSubgrupo},
            {path:'completardatosejercicio/:uidejercicio', component:CompletarDatosEjercicio},
            {path:'sesionentrenamiento/:uiddia', component:SesionEntrenamiento},
            {path:'estadousuario/:uiddia',component:EstadoUsuarioSesion},
            {path:'registrarserie/:serie/:ejercicio/:uiddia',component:RegistroSerie},
            {path:'editarmesociclo',component:EditarMesociclo},
            {path:'nuevomesociclo/:uidrutina/:nombrerutina',component:CrearRutina},
            {path:'nuevomesociclo/:uidrutina/:nombrerutina/:fechainicio/:fechafin',component:CrearRutina},
            {path:'graficos',component:Graficos}
            
        ]

    },
    
    {path:'**',component:Loginlayout,
           children:[
            { path: '', component: Login },
        ]
    }

];
