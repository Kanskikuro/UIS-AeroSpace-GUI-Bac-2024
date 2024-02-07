import { Canvas } from '@react-three/fiber'
import { useGLTF, Stage, PresentationControls } from '@react-three/drei'

function ModelCar (props: any){
    const { scene } = useGLTF('/bmw.glb')
    return <primitive object={scene} {...props} />
}
function Rocket3Model() {
    return (
        <Canvas dpr={[1,2]} camera={{ fov: 35 }}>
            <color attach={'background'} args={['#101010']} />
            <PresentationControls speed={1.5} global zoom={0.5} polar={[-0.1, Math.PI/4]}>
                <Stage environment={null}>
                    <ModelCar scale={0.01}/>
                </Stage>
            </PresentationControls>
        </Canvas>
    )
}
export default Rocket3Model